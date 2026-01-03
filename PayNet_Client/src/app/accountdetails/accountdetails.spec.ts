import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Accountdetails } from './accountdetails';

describe('Accountdetails', () => {
  let component: Accountdetails;
  let fixture: ComponentFixture<Accountdetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Accountdetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Accountdetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
