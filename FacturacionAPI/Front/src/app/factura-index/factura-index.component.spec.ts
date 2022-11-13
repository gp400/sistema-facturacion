import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FacturaIndexComponent } from './factura-index.component';

describe('FacturaIndexComponent', () => {
  let component: FacturaIndexComponent;
  let fixture: ComponentFixture<FacturaIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FacturaIndexComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FacturaIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
