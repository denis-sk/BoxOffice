import { Component, OnInit, TemplateRef } from '@angular/core';
import { ShowClient, SessionClient, SessionsClient, ShowsVm, SessionsVm, SessionDto, ShowDto, UpdateShowCommand, UpdateSessionCommand, CreateShowCommand, CreateSessionCommand } from '../boxoffice-api';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal/';
import { faEdit, faPlus } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-show-manager',
  templateUrl: './show-manager.component.html',
  styleUrls: ['./show-manager.component.css']
})
export class ShowManagerComponent implements OnInit {

  showsVm: ShowsVm;
  // sessionsVm: SessionsVm;

  selectedSession: SessionDto;
  selectedShow: ShowDto;

  search: string;
  page: number = 1;
  month: number;
  limit: number = 20;

  faEdit = faEdit;
  faPlus = faPlus;

  updateSessionModalRef: BsModalRef;
  updateShowModalRef: BsModalRef;
  createSessionModalRef: BsModalRef;
  createShowModalRef: BsModalRef;

  sessionEditor: any = {};
  showEditor: any = {};

  newShowEditor: any = {};
  newSessionEditor: any = {};

  constructor(
    private showClient: ShowClient,
    private sessionClient: SessionClient,
    private sessionsClient: SessionsClient,
    private modalService: BsModalService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {

    this.showClient.get().subscribe(
      request => {
        this.showsVm = request;
      },
      error => {

      }
    );
  }

  showUpdateModal(template: TemplateRef<any>, item: ShowDto): void {
    this.selectedShow = item;
    this.showEditor = {
      ...this.selectedShow
    };
    this.updateShowModalRef = this.modalService.show(template);
  }
  sessionUpdateModal(template: TemplateRef<any>, item: SessionDto): void {
    this.selectedSession = item;
    this.sessionEditor = {
      ...this.selectedSession
    };
    
    this.updateSessionModalRef = this.modalService.show(template);
  }
  createShowModal(template: TemplateRef<any>) {
    this.createShowModalRef = this.modalService.show(template);
  }
  createSessionModal(template: TemplateRef<any>, show: ShowDto) {
    this.selectedShow = show;
    this.createSessionModalRef = this.modalService.show(template);
  }

  updateShowOptions() {
    this.showClient.update(this.showEditor.id, UpdateShowCommand.fromJS(this.showEditor)).subscribe(
      () => {
        this.selectedShow.title = this.showEditor.title;
        this.updateShowModalRef.hide();
        this.showEditor = {};
      },
      error => {
        let resp = JSON.parse(error.response);

        if (resp && resp.errors) {
          this.showEditor.errors = resp.errors;
        }
      }
    )
  }
  updateSessionOptions() {
    console.log(this.sessionEditor)
    this.sessionClient.update(this.sessionEditor.id, UpdateSessionCommand.fromJS(this.sessionEditor)).subscribe(
      () => {
        this.selectedSession.time = this.sessionEditor.time;
        this.selectedSession.placesLimit = this.sessionEditor.placesLimit;
        this.updateSessionModalRef.hide();
        this.sessionEditor = {};
      },
      error => {
        let resp = JSON.parse(error.response);

        if (resp && resp.errors) {
          this.sessionEditor.errors = resp.errors;
        }
      }
    )
  }
  newShowCancelled(): void {
    this.createShowModalRef.hide();
    this.newShowEditor = {};
  }
  newSessionCancelled(): void {
    this.createSessionModalRef.hide();
    this.newSessionEditor = {};
  }

  createShow(): void {
    let show = ShowDto.fromJS({
      title: this.newShowEditor.title
    });
    this.showClient.create(<CreateShowCommand>{ title: this.newShowEditor.title }).subscribe(
      result => {
        show.id = result;
        this.showsVm.list.push(show);
        this.createShowModalRef.hide();
        this.newShowEditor = {};
      },
      error => {
        let resp = JSON.parse(error.response);

        if (resp && resp.errors) {
          this.newShowEditor.errors = resp.errors;
        }
      }
    );
  }
  createSession(): void {
    let session = SessionDto.fromJS({
      time: this.newSessionEditor.time,
      placesLimit: this.newSessionEditor.placesLimit,
      showId: this.selectedShow.id
    });
    this.sessionClient.create(<CreateSessionCommand>{
      time: this.newSessionEditor.time,
      placesLimit: this.newSessionEditor.placesLimit,
      showId: this.selectedShow.id
    }).subscribe(
      result => {
        this.createSessionModalRef.hide();
        this.newSessionEditor = {};
        this.loadData();
      },
      error => {
        let resp = JSON.parse(error.response);

        if (resp && resp.errors) {
          this.newSessionEditor.errors = resp.errors;
        }
      }
    );
  }
  parseErrors(errors: any) {
    let e = [];
    let fields = Object.keys(errors);
    fields.map(f => errors[f].map(er => e.push(er)));
    return e;
  }
}
