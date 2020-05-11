import { Component, OnInit, TemplateRef } from '@angular/core';
import { ShowClient, SessionClient, SessionVm, OrderClient, CreateOrderCommand, SessionsClient, SessionsVm } from '../boxoffice-api';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Observable } from 'rxjs';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import { BsDatepickerViewMode } from 'ngx-bootstrap/datepicker/models';

@Component({
  selector: 'app-show',
  templateUrl: './sessions.component.html',
  styleUrls: ['./sessions.component.css']
})
export class SessionsComponent implements OnInit {


  debug: boolean;
  public isAuthenticated: Observable<boolean>;

  sessionsVm: SessionsVm;

  selectedSession: SessionVm;

  searchText: string;
  page: number = 1;
  month: number;
  limit: number = 2;

  minMode: BsDatepickerViewMode = 'month';

  bsConfig: Partial<BsDatepickerConfig>;

  faCalendar = faCalendar;

  pageList: number[] = [];

  sessionsModalRef: BsModalRef;

  bookCount: number = 1;

  constructor(
    private showClient: ShowClient,
    private sessionClient: SessionClient,
    private sessionsClient: SessionsClient,
    private orderClient: OrderClient,
    private modalService: BsModalService,
    private authorizeService: AuthorizeService
  ) {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.loadData();
  }

  ngOnInit(): void {
    this.bsConfig = Object.assign({}, {
      minMode: this.minMode,
      adaptivePosition: true
    });
  }
  showSessionModal(template: TemplateRef<any>, session: SessionVm) {
    console.log(this.isAuthenticated)
    if (this.isAuthenticated) {

    }
    this.sessionsModalRef = this.modalService.show(template);
    this.sessionClient.get(session.id).subscribe(
      result => {
        this.selectedSession = result
        console.log(result)
      },
      error => console.error(error)
    )
  }
  loadData() {
    this.sessionsClient.get(this.searchText, this.month, this.page, this.limit).subscribe(
      result => {
        this.sessionsVm = result;
        let pages = [];
        for (var i = 1; i <= Math.ceil(this.sessionsVm.total / this.limit); i++) {
          pages.push(i);
        }
        this.pageList = pages;
      },
      error => console.error(error)
    );
  }
  changePage(page: number) {
    this.page = page;
    this.loadData();
  }
  createOrder() {

    this.orderClient.create(<CreateOrderCommand>{ sessionId: this.selectedSession.id, count: this.bookCount }).subscribe(
      result => {
        this.sessionsModalRef.hide();
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          console.log(errors.Title[0]);
        }
      }
    )
  }
  changeDate(date: Date) {
    if (date)
      this.month = date.getMonth() + 1;
  }
  clearFilter() {
    this.month = undefined;
    this.searchText = undefined
    this.loadData();
  }
}
