import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-thread-list-item',
  templateUrl: './thread-list-item.component.html',
  styleUrls: ['./thread-list-item.component.css']
})
export class ThreadListItemComponent implements OnInit {
  @Input() thread: Thread;

  constructor() { }

  ngOnInit() {
  }

}
