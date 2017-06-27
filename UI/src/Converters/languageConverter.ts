import { Language } from "../kinnser";

export class LanguageValueConverter {
    
    /** Converts values from the datalayer to the View layer */
    toView(value) {
        return Language.Get(value);
    }
}