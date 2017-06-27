import { BaseModule, Language } from "./kinnser";

export class Welcome extends BaseModule {
  constructor() {
    super(Language.Get("Welcome.Header"));
  }
}