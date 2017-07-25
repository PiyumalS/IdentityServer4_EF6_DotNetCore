
import { Injectable, ErrorHandler } from "@angular/core";
import { AlertService, MessageSeverity } from './services/alert.service';


@Injectable()
export class AppErrorHandler extends ErrorHandler {

    constructor() {
        super(true);
    }


    handleError(error) {

        if (confirm("Fatal Error!\nAn unresolved error has occured. Do you want to reload the page to correct this?\n\nError: " + error.message))
            window.location.reload(true);

        super.handleError(error);
    }
}