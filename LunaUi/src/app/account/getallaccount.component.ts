import { Component, OnInit } from "@angular/core";
import { IAllAccount, IAllAccountModel } from '../interface/IAllAccount';
import { AccountService } from './AccountServices';

@Component({
    selector: "pm-getallaccount",
    templateUrl:"./getallaccount.component.html"
})
export class GetAllAccountComponent implements OnInit{
    AllAccount:IAllAccount[]
    Account: IAllAccountModel
    nextpage:string
  previous:string
    pageSize: number
    pageIndex:number
    errorMessage: any;
    NavigatePaginationNext: any
    NavigatePaginationPrevious :any
    constructor(private accountService: AccountService) {
        
    }

    ngOnInit(): void {
    //subscrib to the request and assigned all the value to all account property 
        this.accountService.LoadAllAccount(this.pageSize,this.pageIndex).subscribe({
            next: allAccount => {
                this.Account = allAccount;
                this.AllAccount = this.Account.accountModels
                this.nextpage = this.Account.nextpage,
                    this.previous = this.Account.previousPage
            
            },
            error: err => this.errorMessage = err
        }); 
        this.NavigatePaginationNext = (next) => {
            var index = next[next.length - 1];
           console.log(index)
            this.accountService.LoadAllAccount(10,index).subscribe({
                next: allAccount => {
                    this.Account = allAccount;
                    this.AllAccount = this.Account.accountModels
                    this.nextpage = this.Account.nextpage,
                        this.previous = this.Account.previousPage
                   
                
                },
                error: err => this.errorMessage = err
            }); 
        }
        this.NavigatePaginationPrevious = (previous) => {
            var index = previous[previous.length - 1];
            
            this.accountService.LoadAllAccount(10,index).subscribe({
                next: allAccount => {
                    this.Account = allAccount;
                    this.AllAccount = this.Account.accountModels
                    this.nextpage = this.Account.nextpage,
                        this.previous = this.Account.previousPage
                   
                
                },
                error: err => this.errorMessage = err
            }); 
     }
     
    }
 
   
   
}