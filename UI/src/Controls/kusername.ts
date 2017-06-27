/**
 * Author           Date            Comment
 * ===================================================================================================
 * Ronnie Barnard   6/15/2017       Initial Design
 * 
 */

import { customElement, bindable } from 'aurelia-framework';
import { ValidationRules } from "aurelia-validation";

/** Kinnser Username Control */
@customElement("kusername")
export class kusername {
    @bindable label: string = "";
    @bindable placeholder: string = "";
    @bindable value: any = "";
}

/** Validation Rules for Username */
ValidationRules.ensure("value").required().minLength(7).maxLength(200).on(kusername);
