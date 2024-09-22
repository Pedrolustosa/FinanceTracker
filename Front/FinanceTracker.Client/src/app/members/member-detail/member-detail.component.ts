import { DatePipe, NgIf } from "@angular/common";
import { Component, inject, OnInit, ViewChild } from "@angular/core";
import { GalleryItem, GalleryModule, ImageItem } from "ng-gallery";
import { TabDirective, TabsetComponent, TabsModule } from "ngx-bootstrap/tabs";
import { TimeagoModule } from "ngx-timeago";
import { MessageService } from "../../_services/message.service";
import { MembersService } from "../../_services/members.service";
import { ActivatedRoute } from "@angular/router";
import { Member } from "../../_models/member";
import { Message } from "../../_models/message";
import { MemberMessagesComponent } from "../member-messages/member-messages.component";

@Component({
  selector: 'app-member-detail',
  standalone: true,
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css',
  imports: [TabsModule, GalleryModule, TimeagoModule, DatePipe, MemberMessagesComponent]
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs?: TabsetComponent;
  private messageService = inject(MessageService);
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  member: Member = {} as Member;
  images: GalleryItem[] = [];
  activeTab?: TabDirective;
  messages: Message[] = [];

  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => {
        this.member = data['member'];
        this.member && this.member.photos.map(p => {
          this.images.push(new ImageItem({src: p.url, thumb: p.url}))
        })
      }
    })

    this.route.queryParams.subscribe({
      next: params => {
        params['tab'] && this.selectTab(params['tab'])
      }
    })
  }

  onUpdateMessages(event: Message) {
    this.messages.push(event);
  }

  selectTab(heading: string) {
    if (this.memberTabs) {
      const messageTab = this.memberTabs.tabs.find(x => x.heading === heading);
      if (messageTab) messageTab.active = true;
    }
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages' && this.messages.length === 0 && this.member) {
      this.messageService.getMessageThread(this.member.username).subscribe({
        next: messages => this.messages = messages
      })
    }
  }
}
