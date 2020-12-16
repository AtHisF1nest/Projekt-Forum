import {Component, Inject} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {AuthenticationService} from "../_services/authentication.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  threads: Thread[];
  newThread: any = {};
  threadDetail: ThreadDetail;
  currentThreads: number = 0;
  searchText: string = '';
  newTag: Tag = {color: 'gray', name: ''};
  tags: Tag[];
  selectedTagId: number = 1;
  filteredTags: Tag[];

  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string,
              private authService: AuthenticationService) {
  }

  ngOnInit() {
    this.getThreads();
    this.getTags();
  }

  addThread() {
    this.newThread.user = this.authService.currentUserValue;
    this.http.post<Thread>(this.baseUrl + "threads", this.newThread).subscribe(value => {
      this.threads.push(value);
    });
    this.newThread = {};
  }

  getThreads(canGetPopular: boolean = false, canGetWithoutPosts: boolean = false, searchText: string = '') {
    if (canGetPopular) {
      this.currentThreads = 1;
    } else if (canGetWithoutPosts) {
      this.currentThreads = 2;
    } else {
      this.currentThreads = 0;
    }

    const params = new HttpParams({
      fromObject: {
        canGetPopular: canGetPopular.toString(),
        canGetWithoutPosts: canGetWithoutPosts.toString(),
        searchText: searchText,
        tagId: this.selectedTagId.toString()
      }
    });
    this.http.get<Thread[]>(this.baseUrl + "threads", {params})
      .subscribe(value => this.threads = value);
  }

  deleteThread(id: number) {
    this.http.delete(this.baseUrl + "threads/" + id).subscribe(value => {
    });
    this.threads = this.threads.filter(value => value.id !== id);
  }

  getThreadDetail(id: number) {
    this.http.get<ThreadDetail>(this.baseUrl + "threads/" + id).subscribe(value => this.threadDetail = value);
  }

  searchForThreads() {
    this.getThreads(false, false, this.searchText);
  }

  addTag() {
    this.http.post<Tag>(this.baseUrl + 'tag', this.newTag).subscribe(value => {
      this.tags.push(value);
      this.newTag = {name: '', color: 'gray'};
    });
  }

  getTags() {
    this.http.get<Tag[]>(this.baseUrl + 'tag').subscribe(value => {
      this.tags = value;
      this.filteredTags = value.filter(value1 => value1.id !== 1);
    })
  }

  selectTag(id: number) {
    this.selectedTagId = id;
    this.getThreads();
  }
}
