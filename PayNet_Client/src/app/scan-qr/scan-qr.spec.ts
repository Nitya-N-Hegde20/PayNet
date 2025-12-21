import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScanQR } from './scan-qr';

describe('ScanQR', () => {
  let component: ScanQR;
  let fixture: ComponentFixture<ScanQR>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScanQR]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScanQR);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
