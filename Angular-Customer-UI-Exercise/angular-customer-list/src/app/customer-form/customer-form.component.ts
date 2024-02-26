import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [ FormsModule, ReactiveFormsModule, NavBarComponent ],
  templateUrl: './customer-form.component.html',
  styleUrl: './customer-form.component.css'
})
export class CustomerFormComponent {

  formData = this.formBuilder.group({
    name: ['', Validators.required],
    phone: ['', [Validators.required, Validators.pattern('^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$')]]
  })
 
  constructor (
    private formBuilder: FormBuilder,
    private router : Router,
    private titleService : Title
  ) {
    this.titleService.setTitle("Add Customer");
  }

  determineId(customerData : any) {
    var currentId = customerData[customerData.length - 1].id;
    var currentIdNumber = Number(currentId);
    return currentIdNumber += 1;
  }

  addToStorage(customerDataString: string) {
    
    var customerData = JSON.parse(customerDataString);
    var id = this.determineId(customerData)
  
    // Create new JSON Object, push into array and save to localStorage.
    var newCustomer = {id: id, name: this.formData.value.name, phone: this.formData.value.phone};
    customerData.push(newCustomer);
    localStorage.setItem('customerData', JSON.stringify(customerData));
  }

  createStorage() {

    // Create JSON Array and push to localStorage.
    var newCustomer = {id: 1, name: this.formData.value.name, phone: this.formData.value.phone};
    var newCustomerArray = Array.of(newCustomer);
    localStorage.setItem('customerData', JSON.stringify(newCustomerArray));
  }
  
  onSubmit() : void {

    var customerDataString = localStorage.getItem('customerData');

    // See if dataset is currently empty
    if(customerDataString !== null)
      this.addToStorage(customerDataString);
    else
      this.createStorage();

    this.formData.reset();
    this.router.navigate(['./customer-list']);
  }
}