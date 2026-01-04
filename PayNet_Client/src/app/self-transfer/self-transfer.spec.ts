import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelfTransfer } from './self-transfer';

describe('SelfTransfer', () => {
  let component: SelfTransfer;
  let fixture: ComponentFixture<SelfTransfer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelfTransfer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelfTransfer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
