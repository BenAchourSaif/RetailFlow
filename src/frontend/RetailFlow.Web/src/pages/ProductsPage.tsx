import { useEffect, useState, type FormEvent } from "react";

import { apiGet, apiPost } from "../api/client";

import type {
    CreateProductRequest,
    Product,
} from "../types/product";

export default function ProductsPage() {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const [form, setForm] = useState<CreateProductRequest>({
        sku: "",
        name: "",
        sellingPrice: 0,
        vatRate: 19,
    });

    const [creating, setCreating] = useState(false);

    useEffect(() => {
        let cancelled = false;

        async function fetchProducts() {
            try {
                const data = await apiGet<Product[]>("/api/products");

                if (cancelled) {
                    return;
                }

                setProducts(data);
                setError("");
            } catch (err) {
                if (cancelled) {
                    return;
                }

                setError(
                    err instanceof Error
                        ? err.message
                        : "Failed to load products."
                );
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        }

        fetchProducts();

        return () => {
            cancelled = true;
        };
    }, []);

    function handleChange(
        event: React.ChangeEvent<HTMLInputElement>
    ) {
        const { name, value } = event.target;

        setForm((current) => ({
            ...current,
            [name]:
                name === "sellingPrice" || name === "vatRate"
                    ? Number(value)
                    : value,
        }));
    }

    async function handleSubmit(event: FormEvent) {
        event.preventDefault();

        try {
            setCreating(true);
            setError("");

            await apiPost<CreateProductRequest, Product>(
                "/api/products",
                form
            );

            setForm({
                sku: "",
                name: "",
                sellingPrice: 0,
                vatRate: 19,
            });

            const data = await apiGet<Product[]>("/api/products");
            setProducts(data);
        } catch (err) {
            setError(
                err instanceof Error
                    ? err.message
                    : "Failed to create product."
            );
        } finally {
            setCreating(false);
        }
    }

    return (
        <div className="page">
            <div className="page-header">
                <div>
                    <h1>Products</h1>
                    <p>Manage products and pricing.</p>
                </div>
            </div>

            <div className="content-grid">
                <section className="card">
                    <h2>Create product</h2>

                    <form
                        onSubmit={handleSubmit}
                        className="product-form"
                    >
                        <label>
                            SKU
                            <input
                                type="text"
                                name="sku"
                                value={form.sku}
                                onChange={handleChange}
                                required
                            />
                        </label>

                        <label>
                            Name
                            <input
                                type="text"
                                name="name"
                                value={form.name}
                                onChange={handleChange}
                                required
                            />
                        </label>

                        <label>
                            Selling price
                            <input
                                type="number"
                                name="sellingPrice"
                                value={form.sellingPrice}
                                onChange={handleChange}
                                min="0"
                                step="0.01"
                                required
                            />
                        </label>

                        <label>
                            VAT rate
                            <input
                                type="number"
                                name="vatRate"
                                value={form.vatRate}
                                onChange={handleChange}
                                min="0"
                                max="100"
                                step="0.01"
                                required
                            />
                        </label>

                        <button type="submit" disabled={creating}>
                            {creating
                                ? "Creating..."
                                : "Create product"}
                        </button>
                    </form>
                </section>

                <section className="card">
                    <h2>Product list</h2>

                    {loading && <p>Loading products...</p>}

                    {!loading && products.length === 0 && (
                        <p>No products found.</p>
                    )}

                    {!loading && products.length > 0 && (
                        <div className="table-container">
                            <table>
                                <thead>
                                    <tr>
                                        <th>SKU</th>
                                        <th>Name</th>
                                        <th>Price</th>
                                        <th>VAT</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    {products.map((product) => (
                                        <tr key={product.id}>
                                            <td>{product.sku}</td>
                                            <td>{product.name}</td>
                                            <td>
                                                {product.sellingPrice.toFixed(2)}
                                            </td>
                                            <td>
                                                {product.vatRate.toFixed(2)}%
                                            </td>
                                            <td>
                                                {product.isActive
                                                    ? "Active"
                                                    : "Inactive"}
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
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