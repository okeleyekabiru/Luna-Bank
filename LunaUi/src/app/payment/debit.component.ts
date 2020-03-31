import { Component, OnInit } from "@angular/core";
import {
  trigger,
  state,
  style,
  animate,
  transition
} from "@angular/animations";
import { FormBuilder } from "@angular/forms";
import { Validators } from "@angular/forms";
import { AccountService } from "../account/AccountServices";
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
@Component({
  selector: "pm-debit",
  templateUrl: "./debit.component.html",
  animations: [
    
    // animation triggers go here
  ]
})
  
export class DebitComponent implements OnInit {
  accountnumbers: [];
  errorMessage: any;
  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private toastr: ToastrService,
    private router:Router
  ) {}
  ngOnInit(): void {
    this.accountService.LoadAllAccountNumber().subscribe({
      next: accountnumbers => {
        this.accountnumbers = accountnumbers;
      }
    });
  }
  accounts: any;
  debit: any
  credit:any
  paymentForm = this.fb.group({
    Amount: ["", Validators.required],
    accountNumber: [""],
    senderAccount: [""]
  });


  OnSubmit(): void{
    var senderBody = {
      Amount: this.paymentForm.value.Amount,
      accountNumber: this.paymentForm.value.accountNumber
      
    };
    var receiverBody = {
      Amount: this.paymentForm.value.Amount,
      accountNumber: this.paymentForm.value.senderAccount
    }
 
    this.accountService.postSender(senderBody).subscribe({
      next: response => {
        this.debit = response
        console.log(response)
        if(response != null)  this.toastr.success("Transaction successful", `${this.paymentForm.value.Amount} as been sent too ${response.data.accountNumber}`);
   
      }, error: err => { 
        this.errorMessage = err
        this.toastr.error("insufficient balance").onHidden
        this.router.navigate(["/welcome"])
        
      }
    })
    this.accountService.postReceiver(receiverBody).subscribe({
      next: response => {
        this.credit = response
      }
    })

  
    
  }
}
