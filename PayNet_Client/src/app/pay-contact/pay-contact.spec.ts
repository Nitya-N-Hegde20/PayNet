import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayContact } from './pay-contact';

describe('PayContact', () => {
  let component: PayContact;
  let fixture: ComponentFixture<PayContact>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PayContact]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PayContact);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
