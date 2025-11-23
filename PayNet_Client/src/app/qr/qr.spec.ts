import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QR } from './qr';

describe('QR', () => {
  let component: QR;
  let fixture: ComponentFixture<QR>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QR]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QR);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
