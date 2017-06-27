import {Security} from "../kinnser";

export class AuthFilterValueConverter {
    toView(routes, isAuthenticated) {
        if (isAuthenticated) {
            return routes.filter(r => r.config.auth && (r.config.settings.roles.length === 0 || (r.config.settings && Security.hasRole(r.config.settings.roles))));
        }
        return routes.filter(r => !r.config.auth);
    }
}