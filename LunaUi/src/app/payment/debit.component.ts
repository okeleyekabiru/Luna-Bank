import { Component } from "@angular/core";
import { trigger,state,style,animate,transition} from "@angular/animations";
import { FormBuilder } from "@angular/forms";
@Component({
  selector: "pm-debit",
  templateUrl: "./debit.component",
  animations: [
    // animation triggers go here
  ]
})
export class DebitComponent {
    constructor(private fb: FormBuilder) { }
}
