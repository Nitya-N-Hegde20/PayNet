import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPhone } from './add-phone';

describe('AddPhone', () => {
  let component: AddPhone;
  let fixture: ComponentFixture<AddPhone>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddPhone]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPhone);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
