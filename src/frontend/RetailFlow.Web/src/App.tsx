import { BrowserRouter, Route, Routes } from "react-router-dom";

import Layout from "./components/Layout";
import DashboardPage from "./pages/DashboardPage";
import ProductsPage from "./pages/ProductsPage";
import InventoryPage from "./pages/InventoryPage";
import SalesPage from "./pages/SalesPage";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route element={<Layout />}>
                    <Route path="/" element={<DashboardPage />} />
                    <Route path="/products" element={<ProductsPage />} />
                    <Route path="/inventory" element={<InventoryPage />} />
                    <Route path="/sales" element={<SalesPage />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;