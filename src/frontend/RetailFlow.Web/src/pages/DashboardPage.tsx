import { useEffect, useState } from "react";

import { apiGet } from "../api/client";

import type { DashboardSummary } from "../types/dashboard";

export default function DashboardPage() {
    const [summary, setSummary] = useState<DashboardSummary | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        let cancelled = false;

        async function fetchDashboard() {
            try {
                const data = await apiGet<DashboardSummary>(
                    "/api/dashboard/summary"
                );

                if (cancelled) {
                    return;
                }

                setSummary(data);
                setError("");
            } catch (err) {
                if (cancelled) {
                    return;
                }

                setError(
                    err instanceof Error
                        ? err.message
                        : "Failed to load dashboard."
                );
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        }

        fetchDashboard();

        return () => {
            cancelled = true;
        };
    }, []);

    return (
        <div className="page">
            <div className="page-header">
                <div>
                    <h1>Dashboard</h1>
                    <p>Overview of your retail activity.</p>
                </div>
            </div>

            {loading && <p>Loading dashboard...</p>}

            {error && (
                <p className="error-message">
                    {error}
                </p>
            )}

            {summary && (
                <>
                    <div className="dashboard-grid">
                        <div className="dashboard-card">
                            <span>Products</span>
                            <strong>{summary.productCount}</strong>
                        </div>

                        <div className="dashboard-card">
                            <span>Active Products</span>
                            <strong>{summary.activeProductCount}</strong>
                        </div>

                        <div className="dashboard-card">
                            <span>Stock Quantity</span>
                            <strong>
                                {summary.inventoryQuantity.toFixed(2)}
                            </strong>
                        </div>

                        <div className="dashboard-card">
                            <span>Inventory Value</span>
                            <strong>
                                {summary.inventoryValue.toFixed(2)}
                            </strong>
                        </div>

                        <div className="dashboard-card">
                            <span>Sales</span>
                            <strong>{summary.salesCount}</strong>
                        </div>

                        <div className="dashboard-card">
                            <span>Margin</span>
                            <strong>
                                {summary.margin.toFixed(2)}
                            </strong>
                        </div>
                    </div>

                    <section className="card">
                        <h2>Sales overview</h2>

                        <div className="sales-overview">
                            <div>
                                <span>Total sales</span>
                                <strong>
                                    {summary.salesAmount.toFixed(2)}
                                </strong>
                            </div>

                            <div>
                                <span>Total cost</span>
                                <strong>
                                    {summary.salesCost.toFixed(2)}
                                </strong>
                            </div>

                            <div>
                                <span>Margin</span>
                                <strong>
                                    {summary.margin.toFixed(2)}
                                </strong>
                            </div>
                        </div>
                    </section>
                </>
            )}
        </div>
    );
}