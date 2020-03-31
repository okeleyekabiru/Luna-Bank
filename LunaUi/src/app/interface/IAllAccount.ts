export interface IAllAccount{
  
        accountId:string
        accountNumber: string,
        createdOn: Date,
        accountType: string,
        status: string,
        balance: number,
        firstName: string,
        lastName: string
    
      
}
export interface IAllAccountModel {
    accountModels: IAllAccount[],
    nextpage: string,
    previousPage: string
}