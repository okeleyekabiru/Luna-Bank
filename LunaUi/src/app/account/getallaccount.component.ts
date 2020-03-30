import { Component, OnInit } from "@angular/core";
import { IAllAccount } from '../interface/IAllAccount';
import { AccountService } from './AccountServices';

@Component({
    selector: "pm-getallaccount",
    templateUrl:"./getallaccount.component.html"
})
export class GetAllAccountComponent implements OnInit{
    AllAccount: IAllAccount[]
    pageSize: number
    pageIndex:number
    errorMessage: any;
    constructor(private accountService: AccountService) {

    }

    ngOnInit(): void {
    
        this.accountService.LoadAllAccount(this.pageSize,this.pageIndex).subscribe({
            next: allAccount => {
              this.AllAccount = allAccount;
              console.log(allAccount)
            },
            error: err => this.errorMessage = err
        });
        
    }
 
   
   
}