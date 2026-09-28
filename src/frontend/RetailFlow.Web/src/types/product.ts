export interface Product {
    id: string;
    sku: string;
    name: string;
    sellingPrice: number;
    vatRate: number;
    isActive: boolean;
}

export interface CreateProductRequest {
    sku: string;
    name: string;
    sellingPrice: number;
    vatRate: number;
}