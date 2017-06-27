import { Language, Storage, Events, User, Nav, Server } from './kinnser';
import { bindable, autoinject } from "aurelia-framework";
import { MessageBox } from './messagebox';
import { Router } from 'aurelia-router';

@autoinject
export class NavBar {
    isAuthenticated: boolean;
    hasNotifications: boolean;
    routes = [];
    name = "";

    constructor(private router: Router) {
        Events.Register("SignedIn", this, this.signedIn);
        Events.Register("SignedOut", this, this.signedOut);
        this.routes = [];
        this.routes = this.router.navigation;
    }

    signedIn(sender, param: User) {
        sender.isAuthenticated = true;
        (<any>param).isActive = true;
        Storage.Set("User", param);
        Storage.Set("Active", param);
        Storage.Set("Users", [param]);
        sender.routes = [];
        sender.routes = sender.router.navigation;
        sender.name = `${param.Name} ${param.Surname}`;

        sender.isbusy = true;
        setTimeout(() => {
            sender.isbusy = false;
            Nav.goto("welcome");
        }, 100);
    }

    signedOut(sender, param) {
        sender.isAuthenticated = false;
        Storage.Clear();
        Nav.goto("signin");
    }

    signout() {
        MessageBox.Show(Language.Get("Security.AreyouSureSignOut"), Language.Get("MessageBox.QuestionHeader"),
            res => {
                if (res.wasCancelled) return;
                Events.Fire("SignedOut", Storage.Get<User>("Active"));
            }, false, false, true);
    }
}