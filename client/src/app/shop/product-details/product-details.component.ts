import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private shopService: ShopService, private activeteRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }


  loadProduct() {
    this.shopService
    .getProduct(+this.activeteRoute.snapshot.paramMap.get('id')!)
    .subscribe(product => { this.product = product; }, err => console.log(err));
  }
}
