import { TestBed } from '@angular/core/testing';

import { AccountService } from './AccountServices';

describe('AccountServices', () => {
  let service: AccountService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});