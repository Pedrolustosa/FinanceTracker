import { Member } from './../_models/member';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  members = signal<Member[]>([]);

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users/GetUsers').subscribe({
      next: (members) => this.members.set(members),
    });
  }

  getMember(userName: string) {
    const member = this.members().find((x) => x.userName === userName);
    if (member !== undefined) return of(member);
    return this.http.get<Member>(
      this.baseUrl + 'users/GetUserByUsername/' + userName
    );
  }

  updateMember(member: Member) {
    return this.http.put<Member>(this.baseUrl + 'users/UpdateUser', member).pipe(
      tap(() =>  {
        this.members.update(members => members.map(m => m.userName === member.userName ? member : m))
      })
    )
  }
}
