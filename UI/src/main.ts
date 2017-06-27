/**
 * Author           Date            Comment
 * ===================================================================================================
 * Ronnie Barnard   6/15/2017       Initial Design
 * 
 */

import 'bootstrap';
import { Aurelia } from 'aurelia-framework';

export function configure(aurelia: Aurelia) {
  aurelia.use
    .standardConfiguration()
    .developmentLogging();

  aurelia.use.globalResources([
    "components/page.html",
    "controls/kusername",
    "controls/kpassword",
    "controls/klabel.html",
    "controls/kbutton.html"
  ]);

  aurelia.use.plugin('aurelia-animator-css');
  aurelia.use.plugin("aurelia-dialog");
  aurelia.use.plugin("aurelia-kendoui-bridge");
  aurelia.use.plugin('aurelia-validation');

  // Anyone wanting to use HTMLImports to load views, will need to install the following plugin.
  // aurelia.use.plugin('aurelia-html-import-template-loader')

  aurelia.start().then(() => aurelia.setRoot());
}
