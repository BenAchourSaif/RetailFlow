export interface CreateSaleLineRequest {
    productId: string;
    quantity: number;
}

export interface CreateSaleRequest {
    storeId: string;
    lines: CreateSaleLineRequest[];
}

export interface SaleResponse {
    id: string;
    storeId: string;
    totalAmount: number;
    totalCost: number;
    margin: number;
}