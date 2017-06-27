/**
 * Author           Date            Comment
 * ===================================================================================================
 * Ronnie Barnard   6/15/2017       Initial Design
 * 
 */

import "fetch";
import { HttpClient } from "aurelia-fetch-client";
import { Router } from "aurelia-router";
import { MessageBox } from "./messagebox";
import { autoinject } from "aurelia-framework";

export class Utils {
    static urlParams(): any {
        var url = window.location.href;
        var parms = {}, pieces, parts, i;
        var question = url.lastIndexOf("?");
        if (question !== -1) {
            url = url.slice(question + 1);
            pieces = url.split("&");
            for (i = 0; i < pieces.length; i++) {
                parts = pieces[i].split("=");
                if (parts.length < 2) parts.push("");
                parms[decodeURIComponent(parts[0])] = decodeURIComponent(parts[1]);
            }
        }
        return parms;
    }

    static async(call, context) {
        setTimeout(() => call(context), 100);
    }
}

export class Loader {
    _count = 0;
    _func: any;

    constructor(private page: any) { }

    init(value, ret: Function = null) {
        this.page.isbusy = true;
        this.count = value;
        this._func = ret;
    }

    get count() {
        return this._count;
    }

    set count(value) {
        this._count = value;
        if (value <= 0) {
            this.page.isbusy = false;
            if (this._func)
                this._func();
        }
    }
}

export class Guid {
    _GuidValue: string;

    static Empty: Guid = new Guid("00000000-0000-0000-0000-000000000000");

    constructor(guid: string) {
        this._GuidValue = guid;
    }

    private static _s4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }

    static NewGuid(): string {
        return new Guid((this._s4() + this._s4() + "-" + this._s4() + "-4" + this._s4().substr(0, 3) + "-" + this._s4() + "-" + this._s4() + this._s4() + this._s4()).toLowerCase()).toString();
    }

    static emptyAsNull(guid: string) {
        return guid === null || guid === undefined ? Guid.Empty.toString() : guid;
    }

    toString() {
        return this._GuidValue;
    }
}

export class Security {
    static hasRole(roles: string[]) {
        var u = Storage.Get<User>("User");
        var a = Storage.Get<User>("Active");
        if (!a && !u) return false;
        if (u.UserKey !== a.UserKey) {
            if (a.Roles === undefined || a.Roles === null || roles.some(f => f === "VC Admin")) return true;
            return this.userHasRole(a, roles);
        }
        else return this.userHasRole(u, roles);
    }

    static userHasRole(user: User, roles: string[]) {
        if (user === null || user === undefined || user.Roles === null || user.Roles === undefined) return false;
        return user.Roles.some(r => roles.some(s => s.toLocaleLowerCase() === r.toLocaleLowerCase()));
    }
}

export class Server {
    static Send(path, data, done, error) {
        return this.Fetch(path, data, done, error);
    }

    static Fetch(data, done, error, path = "api/generic/get") {
        const user = Storage.Get<User>("User");
        const active = Storage.Get<User>("Active");
        const params = Storage.Get<any>("UrlParams");
        if (user) {
            data.AdminUserKey = user.UserKey;
        }
        if (active) {
            data.ActiveUserKey = active.UserKey;
        }

        new HttpClient().configure(config => { config.withBaseUrl(Storage.Get<string>("api")); })
            .fetch(`/${path}`, { method: "POST", body: Base64.encode(JSON.stringify(data)) })
            .then(res => {
                return res.json();
            })
            .then(data => {
                var d = (data as any);
                if (d.Code !== 250) // OK
                    error(d);
                else
                    done(JSON.parse(Base64.decode(d.Data)));
            })
            .catch(ex => {
                error({ Message: ex.message });
            });
    }
}

export class Base64 {
    static _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    static encode(inValue: string) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        var input = Base64._utf8_encode(inValue);

        while (i < input.length) {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) enc3 = enc4 = 64;
            else if (isNaN(chr3)) enc4 = 64;

            output = output + this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) + this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);
        }
        return output;
    }

    static decode(inString: string) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        var input = inString.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {
            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) output = output + String.fromCharCode(chr2);
            if (enc4 != 64) output = output + String.fromCharCode(chr3);
        }

        output = Base64._utf8_decode(output);

        return output;
    }

    static _utf8_encode(string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);

            if (c < 128) utftext += String.fromCharCode(c);
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }

        return utftext;
    }

    static _utf8_decode(utftext) {
        var string = "";
        var i = 0;
        var c1 = 0;
        var c2 = 0;
        var c3 = 0;
        var c = 0;

        while (i < utftext.length) {
            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }

        return string;
    }
}

export class storeitem {
    constructor(public name: string, public value: any) { }
}

export class eventitem {
    name: string;
    callers = [];

    constructor(name, caller, value) {
        this.name = name;
        this.callers.push({ caller: caller, value: value });
    }
}

export class User {
    UserKey: string;
    Name: string;
    Surname: string;
    ProfileImage: string;
    SocialNumber: string;
    EmailAddress: string;
    PhoneNumber: string;
    hasNotifications: boolean;
    Username: string;
    Password: string;
    isKinnserUser: boolean;
    Roles: string[];
}

export interface IDictionary<T> {
    add(key: string, value: T): void;
    remove(key: string): void;
    containsKey(key: string): boolean;
    keys(): string[];
    values(): T[];
}

export class Dictionary<T>
{
    _keys = [];
    _values: T[] = [];

    constructor(init: { key: string; value: T; }[] = null) {
        if (!init) return;
        for (var x = 0; x < init.length; x++) {
            this[init[x].key] = init[x].value;
            this._keys.push(init[x].key);
            this._values.push(init[x].value);
        }
    }

    add(key: string, value: T) {
        this[key] = value;
        this._keys.push(key);
        this._values.push(value);
    }

    remove(key: string) {
        var index = this._keys.indexOf(key, 0);
        this._keys.splice(index, 1);
        this._values.splice(index, 1);
        delete this[key];
    }

    keys(): string[] {
        return this._keys;
    }

    values(): T[] {
        return this._values;
    }

    get(key: string) {
        if (this.containsKey(key)) return this[key];
        return null;
    }

    containsKey(key: string) {
        if (typeof this[key] === "undefined") return false;
        return true;
    }

    toLookup(): IDictionary<T> {
        return this;
    }

    clear() {
        this.keys().forEach(k => this[k] = undefined);
        this._keys = [];
        this._values = [];
    }
}

export class Events {
    private static Events: eventitem[] = [];

    static Register(name: string, caller: any, callback: Function) {
        var item = this.getEvent(name);
        if (item === undefined || item === null) {
            item = new eventitem(name, caller, callback);
            this.Events.push(item);
        }
        else
            item.callers.push({ caller: caller, value: callback });
    }

    /** Unregister an event */
    static Unregister(name: string, caller: any) {
        var item = this.getEvent(name);
        if (!item) return;
        var index = item.callers.indexOf(caller);
        item.callers.splice(index);
    }

    /** Fire a named event and pass a parameter through */
    static Fire(name: string, parameter: any) {
        var item = this.getEvent(name);
        if (!item) return;
        item.callers.forEach(f => f.value(f.caller, parameter));
    }

    private static getEvent(name: string): eventitem {
        for (var index = 0; index < this.Events.length; index++) {
            var element = this.Events[index];
            if (element.name === name) return element;
        }
        return undefined;
    }
}

export class Storage {
    private static Items: storeitem[] = [];

    /** Sets a Session Storage Item that will be globally retrievable during the users session 
     * @param name The name the storage item will be stored under and interacted with
     * @param value The value you want to store against the Named item
    */
    static Set<T>(name: string, value: any) {
        var item = this.getItem(name);
        if (item === undefined || item === null) {
            item = new storeitem(name, value);
            this.Items.push(item);
        }
        else item.value = value;
        return <T>item.value;
    }

    /** Retrieve and remove the named item from storage 
     * @param name The name of the storage item
    */
    static GetAndRemove<T>(name: string): T {
        var item = this.Get<T>(name);
        this.Remove(name);
        return item;
    }

    /** Gets a stored storage item 
     * @param name The name of the storage item
    */
    static Get<T>(name: string): T {
        var item = this.getItem(name);
        if (item === undefined || item === null) return null;
        return item.value;
    }

    /** Removes a storage item from the internal list 
     * @param name The name of the storage item
    */
    static Remove(name: string) {
        var item = this.getItem(name);
        if (item === undefined || item === null) return;
        var index = this.Items.indexOf(item);
        if (index <= -1) return;
        this.Items.splice(index, 1);
    }

    /** Clear all items from the storage container */
    static Clear() {
        this.Remove("Active");
        this.Remove("Users");
        this.Remove("User");
    }

    /** Private method to get a specific storeitem */
    private static getItem(name: string): storeitem {
        for (var index = 0; index < this.Items.length; index++) {
            var element = this.Items[index];
            if (element.name === name) return element;
        }
        return undefined;
    }
}

export class Nav {
    private static router: any;

    /** Ensure that the Navigation system has been initialized properly 
     * @param router the router you want to associate with the Navigation system
    */
    static EnsureLoaded(router: Router){
        this.router = router;
    }

    /** navigates to a Nav item given a particular router with the ability to add a parameter. Needed for sub page navigation */
    static gotoRoute(router: Router, route, param = null, name = "Parameter") {
        if (param != null) Storage.Set(name, param);
        router.navigate(route);
    }

    /** navigate back inside a given router. Typically used in sub pages */
    static gobacktoRoute(router: Router) {
        router.navigateBack();
    }

    /** navigate the main application to a particular nav item, with the ability to pass a parameter */
    static goto(route, param = null, name = "Parameter") {
        if (param != null) Storage.Set(name, param);
        this.router.navigate(route);
    }

    /** navigate the main application back */
    static goback() {
        this.router.navigateBack();
    }
}

export abstract class BaseModule {
    user: User;
    heading: string = "";
    isbusy: boolean = false;
    errors: Dictionary<string> = new Dictionary<string>();

    /** Constructing the instance of an abstract base module */
    constructor(header) {
        this.heading = header;
        this.user = Storage.Get<User>("Active");
    }
}

export class Language {
    private language: Dictionary<string> = new Dictionary<string>();
    
    /** Represents the memory instance of the Language construct */
    static instance: Language = null;

    /** Constructs the multi language support for the system */
    constructor(private http: HttpClient) { }

    /** Change the current locale of the system */
    Change(locale) {
        this.language.clear();
        this.http.fetch(`/language/${locale}/language.json`)
            .then(res => res.json())
            .then(lang =>{
                lang.items.forEach(element => {
                    this.language.add(element.key, element.value);
                });
                Events.Fire("Reload", null);
            });
    }

    /** Gets the indicated key from the Language, returns the key in case it does not exist in the language spec file */
    static Get(key: string) {
        return this.instance.language.containsKey(key) ? this.instance.language.get(key) : key;
    }

    /** This will instantiate the Language Layer, you have to pass in the Server Connection client */
    static EnsureLoaded(http: HttpClient) {
        Language.instance = new Language(http);
    }
}