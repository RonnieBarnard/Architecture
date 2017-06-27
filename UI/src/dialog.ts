import { autoinject } from 'aurelia-framework';
import { DialogController } from 'aurelia-dialog';
import { Storage } from './kinnser';

@autoinject
export class Dialog {
    content: any;
    isYes: boolean;
    header: string;
    model: string;

    constructor(private controller: DialogController) {
    }

    activate(dialogContent) {
        this.content = dialogContent;
        this.isYes = dialogContent.isYes;
        this.model = dialogContent.model;
        this.header = dialogContent.header;
    }

    save() {
        this.controller.ok(this.content);
    }

    static show(model: string, header: string, result: any, data: any, datakey: string, isYes = true, submittext = "Save", canceltext = "Cancel") {
        Storage.Set(datakey, data);
        (<any>window).dialog.open({ viewModel: Dialog, model: { model: model, header: header, isYes: isYes, submittext: submittext, canceltext: canceltext } }).then(response => { if (result) result(response); });
    }

    static showActivated(model: string, header: string, result: any, data: any, isYes = true, submittext = "Save", canceltext = "Cancel") {
        (<any>window).dialog.open({ viewModel: Dialog, model: { model: model, header: header, data: data, isYes: isYes, submittext: submittext, canceltext: canceltext } }).then(response => { if (result) result(response); });
    }
}