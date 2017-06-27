/**
 * Author           Date            Comment
 * ===================================================================================================
 * Ronnie Barnard   6/15/2017       Initial Design
 * 
 */

import { Language, Storage, Events, Nav, Server, BaseModule,Security } from "../../kinnser";
import { MessageBox } from "../../messagebox";
import { ValidationController } from "aurelia-validation";
import { autoinject } from "aurelia-framework";

@autoinject
export class Signin extends BaseModule {
    username = "";
    password = "";

    /** Kinnser Signin Page */
    constructor(private validationController: ValidationController) {
        super(Language.Get("Login.Page.Header"));
    }

    /** Attempt a signin */
    signin(event) {
        event.preventDefault();

        // Lets validate on all the businessrules and then break if we still have errors
        this.validationController.validate();
        if (this.validationController.errors.length > 0) return;

        // Now set the busy indicator for the form, so that we blank input, and call the backend
        this.isbusy = true;
        Server.Fetch(
            { username: this.username, password: this.password },
            res => {
                this.isbusy = false;
                Events.Fire("SignedIn", res);
            },
            fail => {
                this.isbusy = false;
                MessageBox.ShowException(fail);
            },
            "api/Auth/Login"
        );
    }
}