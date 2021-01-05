import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ThreadListItemComponent } from './thread-list-item.component';

describe('ThreadListItemComponent', () => {
  let component: ThreadListItemComponent;
  let fixture: ComponentFixture<ThreadListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ThreadListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ThreadListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
