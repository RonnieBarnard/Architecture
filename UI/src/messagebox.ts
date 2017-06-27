import { autoinject } from 'aurelia-framework';
import { DialogController } from 'aurelia-dialog';
import { Language } from "./kinnser";

@autoinject
export class MessageBox {
    dialogContent: any;
    isOk: boolean;
    isCancel: boolean;
    isYesNo: boolean;
    header: string;
    message: string;

    constructor(private controller: DialogController) {
    }

    activate(dialogContent) {
        this.dialogContent = dialogContent;
        if (this.dialogContent.isOk === undefined &&
            this.dialogContent.isCancel === undefined &&
            this.dialogContent.isYesNo === undefined)
            this.isOk = true;
        else {
            this.isOk = dialogContent.isOk;
            this.isCancel = dialogContent.isCancel;
            this.isYesNo = dialogContent.isYesNo;
        }

        this.message = dialogContent.message;
        this.header = dialogContent.header;
    }

    static Show(message: string, header: string = "", result: any = null, isOk = true, isCancel = false, isYesNo = false) {
        if (header === ""){
            header = Language.Get("MessageBox.DefaultHeader");
        }
        (<any>window).dialog.open({ viewModel: MessageBox, model: { message: message, header: header, isOk: isOk, isCancel: isCancel, isYesNo: isYesNo } })
            .then(response => { if (result) result(response); });
    }

    static ShowException(exception) {
        this.Show(Language.Get(exception.Message ?  exception.Message : exception.message));
    }
}