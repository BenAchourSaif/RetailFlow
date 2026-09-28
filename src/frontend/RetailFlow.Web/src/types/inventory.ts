export interface Inventory {
    id: string;
    storeId: string;
    productId: string;
    quantity: number;
    averageCost: number;
}

export interface ReceiveStockRequest {
    storeId: string;
    productId: string;
    quantity: number;
    unitCost: number;
}