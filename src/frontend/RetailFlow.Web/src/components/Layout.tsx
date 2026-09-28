import { NavLink, Outlet } from "react-router-dom";

export default function Layout() {
    return (
        <div className="app-layout">
            <aside className="sidebar">
                <div className="sidebar-header">
                    <h2>RetailFlow</h2>
                    <span>Back Office</span>
                </div>

                <nav className="sidebar-nav">
                    <NavLink
                        to="/"
                        end
                        className={({ isActive }) =>
                            isActive ? "nav-link active" : "nav-link"
                        }
                    >
                        Dashboard
                    </NavLink>

                    <NavLink
                        to="/products"
                        className={({ isActive }) =>
                            isActive ? "nav-link active" : "nav-link"
                        }
                    >
                        Products
                    </NavLink>

                    <NavLink
                        to="/inventory"
                        className={({ isActive }) =>
                            isActive ? "nav-link active" : "nav-link"
                        }
                    >
                        Inventory
                    </NavLink>

                    <NavLink
                        to="/sales"
                        className={({ isActive }) =>
                            isActive ? "nav-link active" : "nav-link"
                        }
                    >
                        Sales
                    </NavLink>
                </nav>

                <div className="sidebar-footer">
    <span>Tenant</span>
    <strong>{import.meta.env.VITE_TENANT_ID}</strong>
</div>
            </aside>

<main className="main-content">
    <header className="top-header">
        <div>
            <strong>RetailFlow Back Office</strong>
        </div>

        <div className="tenant-badge">
            Tenant: {import.meta.env.VITE_TENANT_ID}
        </div>
    </header>

    <div className="page-content">
        <Outlet />
    </div>
</main>


        </div>
    );
}