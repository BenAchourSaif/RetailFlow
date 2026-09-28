import { useState, type ChangeEvent, type FormEvent } from "react";

import { apiGet, apiPost } from "../api/client";

import type {
    Inventory,
    ReceiveStockRequest,
} from "../types/inventory";

export default function InventoryPage() {
    const [storeId, setStoreId] = useState("");
    const [productId, setProductId] = useState("");

    const [inventory, setInventory] = useState<Inventory | null>(null);

    const [quantity, setQuantity] = useState(0);
    const [unitCost, setUnitCost] = useState(0);

    const [loading, setLoading] = useState(false);
    const [receiving, setReceiving] = useState(false);
    const [error, setError] = useState("");

    async function loadInventory() {
        if (!storeId || !productId) {
            setError("Store ID and Product ID are required.");
            return;
        }

        try {
            setLoading(true);
            setError("");

            const data = await apiGet<Inventory>(
                `/api/inventory/${storeId}/${productId}`
            );

            setInventory(data);
        } catch (err) {
            setError(
                err instanceof Error
                    ? err.message
                    : "Failed to load inventory."
            );
            setInventory(null);
        } finally {
            setLoading(false);
        }
    }

    async function handleReceive(event: FormEvent) {
        event.preventDefault();

        if (!storeId || !productId) {
            setError("Store ID and Product ID are required.");
            return;
        }

        try {
            setReceiving(true);
            setError("");

            const request: ReceiveStockRequest = {
                storeId,
                productId,
                quantity,
                unitCost,
            };

            const data = await apiPost<
                ReceiveStockRequest,
                Inventory
            >("/api/inventory/receive", request);

            setInventory(data);

            setQuantity(0);
            setUnitCost(0);
        } catch (err) {
            setError(
                err instanceof Error
                    ? err.message
                    : "Failed to receive stock."
            );
        } finally {
            setReceiving(false);
        }
    }

    function handleNumberChange(
        event: ChangeEvent<HTMLInputElement>,
        setter: (value: number) => void
    ) {
        setter(Number(event.target.value));
    }

    return (
        <div className="page">
            <div className="page-header">
                <div>
                    <h1>Inventory</h1>
                    <p>Manage stock and average cost.</p>
                </div>
            </div>

            <div className="content-grid">
                <section className="card">
                    <h2>Stock operation</h2>

                    <form
                        onSubmit={handleReceive}
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
                                    handleNumberChange(
                                        event,
                                        setQuantity
                                    )
                                }
                                required
                            />
                        </label>

                        <label>
                            Unit cost
                            <input
                                type="number"
                                min="0"
                                step="0.01"
                                value={unitCost}
                                onChange={(event) =>
                                    handleNumberChange(
                                        event,
                                        setUnitCost
                                    )
                                }
                                required
                            />
                        </label>

                        <button
                            type="submit"
                            disabled={receiving}
                        >
                            {receiving
                                ? "Receiving..."
                                : "Receive stock"}
                        </button>
                    </form>
                </section>

                <section className="card">
                    <h2>Inventory status</h2>

                    <div className="inventory-search">
                        <input
                            type="text"
                            placeholder="Store ID"
                            value={storeId}
                            onChange={(event) =>
                                setStoreId(event.target.value)
                            }
                        />

                        <input
                            type="text"
                            placeholder="Product ID"
                            value={productId}
                            onChange={(event) =>
                                setProductId(event.target.value)
                            }
                        />

                        <button
                            type="button"
                            onClick={loadInventory}
                            disabled={loading}
                        >
                            {loading
                                ? "Loading..."
                                : "Check stock"}
                        </button>
                    </div>

                    {inventory && (
                        <div className="inventory-summary">
                            <div>
                                <span>Quantity</span>
                                <strong>
                                    {inventory.quantity.toFixed(2)}
                                </strong>
                            </div>

                            <div>
                                <span>Average Cost</span>
                                <strong>
                                    {inventory.averageCost.toFixed(4)}
                                </strong>
                            </div>
                        </div>
                    )}

                    {!inventory && !loading && (
                        <p>
                            Enter a store and product to check inventory.
                        </p>
                    )}

                    {error && (
                        <p className="error-message">
                            {error}
                        </p>
                    )}
                </section>
            </div>
        </div>
    );
}