import { Routes, Route } from 'react-router-dom'

import CategoryPage from '../Components/Admin/CategoryPage'
import ProductPage from '../Components/Admin/ProductPage'
import ProductDetail from '../Components/Admin/ProductDetail'
import ManagerPage from '../Components/Admin/ManagerPage'
import OrdersPage from '../Components/Admin/OrdersPage'

import HomePage from '../Components/Client/HomePage'

import LoginPage from '../Components/GeneralComponent/LoginPage'
import RegisterPage from '../Components/GeneralComponent/RegisterPage'
export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/kategoriler" element={<CategoryPage />} />
            <Route path="/urunler" element={<ProductPage />} />
            <Route path="/urun-detay/:id" element={<ProductDetail />} />
            <Route path="/siparisler" element={<OrdersPage />} />
            <Route path="/yonetim" element={<ManagerPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/home" element={<HomePage />} />
        </Routes>
    )
}