import {Component, Inject, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthenticationService} from "../_services/authentication.service";

@Component({
  selector: 'app-thread-detail',
  templateUrl: './thread-detail.component.html',
  styleUrls: ['./thread-detail.component.css']
})
export class ThreadDetailComponent implements OnInit {
  @Input() threadDetail: ThreadDetail;
  newPost: any = {};
  newLike: Like = {id: 0, authorId: 0, likedPostId: 0};

  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string,
              private authService: AuthenticationService) {
  }

  ngOnInit() {
  }

  addPost() {
    this.newPost.user = this.authService.currentUserValue;
    this.newPost.threadId = this.threadDetail.thread.id;

    this.http.post<Post>(this.baseUrl + "posts", this.newPost).subscribe(value => {
      this.threadDetail.posts.push(value);
      this.newPost = {};
      this.threadDetail.thread.postCount += 1;
    })
  }

  likePost(post: Post) {
    this.newLike.authorId = this.authService.currentUserValue.id;
    this.newLike.likedPostId = post.id;

    this.http.post<Like>(this.baseUrl + "likes", this.newLike).subscribe(value => {
      if (value.id === 0) {
        post.likeCount -= 1;
      }
      else {
        post.likeCount += 1;
      }
    }, error => {
      console.log(error);
    })

    this.newLike = {id: 0, authorId: 0, likedPostId: 0};
  }

  replyToPost(post: Post) {
    this.newPost.message = `Cytat \'${post.user.login}\':
    > ${post.message}
`;
  }

  deletePost(id: number) {
    this.http.delete(this.baseUrl + 'posts/' + id).subscribe(value => {
      this.threadDetail.posts = this.threadDetail.posts.filter(value => value.id !== id);
    });
  }
}
