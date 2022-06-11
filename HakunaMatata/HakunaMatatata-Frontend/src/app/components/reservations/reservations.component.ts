import { AfterViewInit, Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Methods } from 'src/app/methods/methods';
import { IReservation } from 'src/app/models/reservation';
import { ApiService } from 'src/app/services/api.service';
import { EditReservationComponent } from '../edit-reservation/edit-reservation.component';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit, AfterViewInit {

  reservations!: Observable<IReservation[]>;
  currentDate = new Date();
  showEditForm = false;
  displayedColumns = ['checkin', 'checkout', 'property', 'totalPrice', 'guests', 'buttons']

  dialogConfig = new MatDialogConfig();
  modalDialog!: MatDialogRef<EditReservationComponent, any>;

  constructor(private api: ApiService, private router: Router, private matDialog: MatDialog) { }

  ngAfterViewInit(): void {
    document.onclick = (args: any): void => {
      if (args.target.tagName === 'BODY') {
        this.modalDialog?.close();
      }
    }
  }

  openModal(reservationId: number) {
    this.dialogConfig.id = "projects-modal-component";
    this.dialogConfig.height = "300px";
    this.dialogConfig.width = "620px";
    this.modalDialog = this.matDialog.open(EditReservationComponent, this.dialogConfig);
    this.modalDialog.componentInstance.reservationId = reservationId;
    this.modalDialog.afterClosed().subscribe(res => this.ngOnInit());
  }

  ngOnInit(): void {
    this.reservations = this.api.getReservations();
  }

  formatDate(d: Date): Date {
    return Methods.formatDate(d);
  }

  checkDate(d: Date): boolean {
    return (Methods.formatDate(d) < Methods.formatDate(this.currentDate))
  }

  delete(reservationId: number): void {
    this.api.deleteReservation(reservationId).subscribe(res => this.reservations = this.api.getReservations());
  }
}
