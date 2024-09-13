import { Member } from './../_models/member';
import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { of, tap } from 'rxjs';
import { Photo } from '../_models/photo';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  // members = signal<Member[]>([]);
  paginatedResult = signal<PaginatedResult<Member[]> | null>(null);

  getMembers(pageNumber?: number, pageSize?: number) {
    let params = new HttpParams();
    if(pageNumber && pageSize){
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }
    return this.http.get<Member[]>(this.baseUrl + 'users/GetUsers', {observe: 'response', params}).subscribe({
      next: response => this.paginatedResult.set({
        items: response.body as Member[],
        pagination: JSON.parse(response.headers.get('Pagination')!)
      }),
    });
  }

  getMember(userName: string) {
    // const member = this.members().find((x) => x.userName === userName);
    // if (member !== undefined) return of(member);
    return this.http.get<Member>(
      this.baseUrl + 'users/GetUserByUsername/' + userName
    );
  }

  updateMember(member: Member) {
    return this.http
      .put<Member>(this.baseUrl + 'users/UpdateUser', member)
      .pipe(
        // tap(() => {
        //   this.members.update((members) =>
        //     members.map((m) => (m.userName === member.userName ? member : m))
        //   );
        // })
      );
  }

  setMainPhoto(photo: Photo) {
    return this.http
      .put(this.baseUrl + 'users/set-main-photo/' + photo.id, {})
      .pipe(
        // tap(() => {
        //   this.members.update((members) =>
        //     members.map((m) => {
        //       if (m.photos.includes(photo)) {
        //         m.photoUrl = photo.url;
        //       }
        //       return m;
        //     })
        //   );
        // })
      );
  }

  deletePhoto(photo: Photo) {
    return this.http
      .delete(this.baseUrl + 'users/delete-photo/' + photo.id)
      .pipe(
      // tap(() => {
      //   this.members.update(members => members.map(m => {
      //     if(m.photos.includes(photo)){
      //       m.photos = m.photos.filter(x => x.id !== photo.id)
      //     }
      //     return m;
      //   }))
      // })
      );
  }
}
