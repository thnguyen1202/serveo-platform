import type { ProductCreateFieldValues } from "./menu.schema";

export interface CategoryCreateRequest {
  name: string;
}

export interface CategoryCreateResponse {
  id: string;
  name: string;
}

export interface MenuCreateRequest {
  name: string;
  description?: string;
}

export interface MenuCreateResponse {
  id: string;
  name: string;
}

export interface MenuOptionResponse {
  id: string;
  name: string;
  itemCount: number;
}


export type ProductCreateRequest = ProductCreateFieldValues;