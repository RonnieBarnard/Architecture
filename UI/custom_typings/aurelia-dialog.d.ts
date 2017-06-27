declare module 'aurelia-dialog' {
    export class DialogService {
        wasCancelled: boolean;
        output: any;
        open(settings: any): Promise<DialogService>;
    }
    export class DialogController {
        constructor(renderer, settings, resolve, reject);
        ok(result: any): Promise<DialogResult>;
        cancel(result: any): Promise<DialogResult>;
        error(message): Promise<DialogResult>;
        close(ok: boolean, result: any): Promise<DialogResult>;
        settings: { lock: boolean, centerHorizontalOnly: boolean, model; any };
    }

    export class DialogResult {
        wasCancelled: boolean;
        output: any;
        constructor(cancelled: boolean, result: any);
    }
}