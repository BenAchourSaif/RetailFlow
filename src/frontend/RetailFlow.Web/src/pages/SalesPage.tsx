import { useState, type FormEvent } from "react";

import { apiPost } from "../api/client";

import type {
    CreateSaleRequest,
    SaleResponse,
} from "../types/sale";

export default function SalesPage() {
    const [storeId, setStoreId] = useState("");
    const [productId, setProductId] = useState("");
    const [quantity, setQuantity] = useState(1);

    const [sale, setSale] = useState<SaleResponse | null>(null);

    const [selling, setSelling] = useState(false);
    const [error, setError] = useState("");

    async function handleSubmit(event: FormEvent) {
        event.preventDefault();

        if (!storeId || !productId) {
            setError("Store ID and Product ID are required.");
            return;
        }

        try {
            setSelling(true);
            setError("");
            setSale(null);

            const request: CreateSaleRequest = {
                storeId,
                lines: [
                    {
                        productId,
                        quantity,
                    },
                ],
            };

            const response = await apiPost<
                CreateSaleRequest,
                SaleResponse
            >("/api/sales", request);

            setSale(response);
        } catch (err) {
            setError(
                err instanceof Error
                    ? err.message
                    : "Failed to create sale."
            );
        } finally {
            setSelling(false);
        }
    }

    return (
        <div className="page">
            <div className="page-header">
                <div>
                    <h1>Sales</h1>
                    <p>Create sales and track margin.</p>
                </div>
            </div>

            <div className="content-grid">
                <section className="card">
                    <h2>Create sale</h2>

                    <form
                        onSubmit={handleSubmit}
                        className="product-form"
                    >
                        <label>
                            Store ID
                            <input
                                type="text"
                                value={storeId}
                                onChange={(event) =>
                                    setStoreId(event.target.value)
                                }
                                required
                            />
                        </label>

                        <label>
                            Product ID
                            <input
                                type="text"
                                value={productId}
                                onChange={(event) =>
                                    setProductId(event.target.value)
                                }
                                required
                            />
                        </label>

                        <label>
                            Quantity
                            <input
                                type="number"
                                min="0.01"
                                step="0.01"
                                value={quantity}
                                onChange={(event) =>
                                    setQuantity(Number(event.target.value))
                                }
                                required
                            />
                        </label>

                        <button
                            type="submit"
                            disabled={selling}
                        >
                            {selling ? "Processing..." : "Create sale"}
                        </button>
                    </form>

                    {error && (
                        <p className="error-message">
                            {error}
                        </p>
                    )}
                </section>

                <section className="card">
                    <h2>Sale result</h2>

                    {!sale && !selling && (
                        <p>
                            Create a sale to see the result.
                        </p>
                    )}

                    {selling && (
                        <p>
                            Processing sale...
                        </p>
                    )}

                    {sale && (
                        <div className="sale-summary">
                            <div>
                                <span>Total amount</span>
                                <strong>
                                    {sale.totalAmount.toFixed(2)}
                                </strong>
                            </div>

                            <div>
                                <span>Total cost</span>
                                <strong>
                                    {sale.totalCost.toFixed(2)}
                                </strong>
                            </div>

                            <div>
                                <span>Margin</span>
                                <strong>
                                    {sale.margin.toFixed(2)}
                                </strong>
                            </div>

                            <div>
                                <span>Sale ID</span>
                                <strong className="sale-id">
                                    {sale.id}
                                </strong>
                            </div>
                        </div>
                    )}
                </section>
            </div>
        </div>
    );
}