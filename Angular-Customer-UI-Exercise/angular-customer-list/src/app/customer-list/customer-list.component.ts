import { Component } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { Title } from '@angular/platform-browser';

// Customer Data Interface

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [NgFor, NgIf, NavBarComponent],
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.css'
})

export class CustomerListComponent {

  constructor(private titleService : Title) {
    this.titleService.setTitle("Customer List");
  }

  // Not strongly typed but works well enough
  customerData: any[] = [];

  ngOnInit(): void {

    var dataString = localStorage.getItem('customerData');
    if (dataString !== null) {
      this.customerData = JSON.parse(dataString);
    }
  }
}