import { Router, RouterConfiguration } from 'aurelia-router';
import { autoinject, PLATFORM } from "aurelia-framework";
import { DialogService } from "aurelia-dialog";
import { Language, Utils, Storage, Nav, Server, User } from "./kinnser";
import { HttpClient } from "aurelia-fetch-client";
import "fetch";

@autoinject
export class App {
    public router: Router;

    constructor(dialogService: DialogService, private http: HttpClient) {
        (<any>window).dialog = dialogService;
        Language.EnsureLoaded(http);
        
        this.http.fetch("/settings.json")
            .then(res => res.json())
            .then(data => {
                var d = (data as any);
                Storage.Set("api", d.api);
                Storage.Set("locale", d.locale);
                Language.instance.Change(d.locale);
            });
    }

    public configureRouter(config: RouterConfiguration, router: Router) {
        Nav.EnsureLoaded(router);

        config.title = 'Kinnser Software';
        config.addPipelineStep("authorize", AuthorizeStep);

        // Set global timeout to 5 minutes
        PLATFORM.global.setTimeout(300000);
        
        config.map([
            /** Landing Section */
            { route: ['', 'welcome'], name: 'welcome', moduleId: 'welcome', nav: true, title: 'Welcome', auth: true, settings: { roles: []} },

            /** Security Section */
            { route: 'signin', name: 'signin', moduleId: './modules/security/signin', nav: false, title: 'Kinnser Login', auth: false, settings: { roles: []} },
            { route: 'signout', name: 'signout', moduleId: './modules/security/signout', nav: false, title: 'Kinnser Signout', auth: false, settings: { roles: []} },

            /** Billing Manager section */
            { route: 'billingmanager', name: 'billingmanager', moduleId: 'billingmanager', nav: true, title: 'Billing Manager', auth: true, settings: { roles: ["Billing Manager"]} },
        ]);

        Storage.Set("UrlParams", Utils.urlParams());
        this.router = router;
    }
}

@autoinject
class AuthorizeStep {
    constructor(private router: Router) { }

    run(routingContext, next) {
        var isLoggedIn = AuthorizeStep.isLoggedIn();

        if (routingContext.getAllInstructions().some(i => i.config.auth)) {
            if (!isLoggedIn) {
                this.router.navigate("signin");
                return next.cancel();
            }
        }
        return next();
    }

    static isLoggedIn(): boolean {
        return Storage.Get<User>("User") != null;
    }
}