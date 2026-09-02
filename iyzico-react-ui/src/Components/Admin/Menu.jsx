import React from 'react';
import { NavLink } from 'react-router-dom';
import { GrUserManager } from "react-icons/gr";
import { BsCCircleFill } from "react-icons/bs";
import { FaProductHunt } from "react-icons/fa";
import { FaUsersCog } from "react-icons/fa";
import { FaBasketShopping } from "react-icons/fa6";


export default function Menu() {

    return (
        //<div className="menu-all">
        //    <div className="menu-component">
        //        <div className="menu-list">
        //            <div className="list-group">
        //                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
        //                    <span className="navbar-toggler-icon"></span>
        //                </button>
        //                <NavLink to="yonetim" className={({ isActive }) => isActive ? "list-group-item active" : "list-group-item"} aria-current="true">
        //                    Yönetim
        //                </NavLink>
        //                <NavLink to="/" className={({ isActive }) => isActive ? "list-group-item active" : "list-group-item"} aria-current="true">
        //                    Kategori Listesi
        //                </NavLink>
        //                <NavLink to="urun-listesi" className={({ isActive }) => isActive ? "list-group-item active" : "list-group-item"}>Ürün Listesi</NavLink>
        //                <NavLink to="kullanici-islemleri" className={({ isActive }) => isActive ? "list-group-item active" : "list-group-item"}>Kullanıcı İşlemleri</NavLink>
        //                <NavLink to="odeme-islemleri" className={({ isActive }) => isActive ? "list-group-item active" : "list-group-item"}>Ödeme İşlemleri</NavLink>
        //            </div>
        //        </div>
        //    </div>
        //</div>

       
        <div className="menu-all">
            <div className="menu-component">
                <nav className="navbar navbar-expand-lg sticky-top p-0 m-0">
                    <div className="container-fluid p-0 m-0">
                        <button
                            className="navbar-toggler"
                            type="button"
                            data-bs-toggle="offcanvas"
                            data-bs-target="#leftMenu"
                            aria-controls="leftMenu"
                            aria-label="Toggle navigation"
                        >
                            <span className="navbar-toggler-icon"></span>
                        </button>
                       
                       

                        <div
                            className="offcanvas offcanvas-start"
                            tabIndex="-1"
                            id="leftMenu"
                            aria-labelledby="leftMenuLabel">
                            <div className="d-flex">
                                <div className="navbar-brand admin-panel-header"><span>Admin</span> Panel</div>
                            <div className="offcanvas-header">
                                <button
                                    type="button"
                                    className="btn-close"
                                    data-bs-dismiss="offcanvas"
                                    aria-label="Close"
                                ></button>
                            </div>
                            </div>
                          

                            <div className="offcanvas-body mt-2">
                                <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                                    <li className="nav-item">
                                        <NavLink to="yonetim" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"} aria-current="true">
                                            <div className="navlink-icon-p"><GrUserManager /> <p className="m-0 ps-2">Yönetim</p> </div>
                                        </NavLink>
                                    </li>
                                    <li className="nav-item dropdown">
                                        <a className="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                                            <div className="navlink-icon-p"><FaUsersCog /> <span className="m-0 ps-2">Kullanıcı İşlemleri</span> </div>
                                        </a>
                                        <ul className="dropdown-menu">
                                            <li><NavLink className={({ isActive }) => isActive ? "dropdown-item active" : "dropdown-item"}  to="dfdf">Yöneticiler</NavLink></li>
                                            <li><NavLink className={({ isActive }) => isActive ? "dropdown-item active" : "dropdown-item"}  to="hfgf">Kullanıcılar</NavLink></li>
                                        </ul>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink to="kategoriler" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"} aria-current="true">
                                            <div className="navlink-icon-p"><BsCCircleFill /> <p className="m-0 ps-2">Kategoriler</p> </div>
                                        </NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink to="urunler" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"} aria-current="true">
                                            <div className="navlink-icon-p"><FaProductHunt /> <p className="m-0 ps-2">Ürünler</p> </div>
                                        </NavLink>
                                    </li>
                                    <li className="nav-item dropdown">
                                        <a className="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                                            <div className="navlink-icon-p"><FaUsersCog /> <span className="m-0 ps-2">Siparişler</span> </div>
                                        </a>
                                        <ul className="dropdown-menu">
                                            <li><NavLink className={({ isActive }) => isActive ? "dropdown-item active show" : "dropdown-item"} to="siparisler">Tüm Siparişler</NavLink></li>
                                            <li><NavLink className={({ isActive }) => isActive ? "dropdown-item active" : "dropdown-item"} to="hgn">Sipariş Durumları</NavLink></li>
                                            <li><NavLink className={({ isActive }) => isActive ? "dropdown-item active" : "dropdown-item"} to="asd">Bekleyen</NavLink></li>
                                            <li><NavLink className={({ isActive }) => isActive ? "dropdown-item active" : "dropdown-item"} to="mhg">İptal Edilen</NavLink></li>
                                        </ul>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink to="yorumlar" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"} aria-current="true">
                                            <div className="navlink-icon-p"><FaProductHunt /> <p className="m-0 ps-2">Yorumlar</p> </div>
                                        </NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink to="yorumlar" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"} aria-current="true">
                                            <div className="navlink-icon-p"><FaProductHunt /> <p className="m-0 ps-2">Bekleyen Değerlendirmeler</p> </div>
                                        </NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink to="raporlar" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"} aria-current="true">
                                            <div className="navlink-icon-p"><FaProductHunt /> <p className="m-0 ps-2">Raporlar</p> </div>
                                        </NavLink>
                                    </li>
                                   
                                </ul>

                              
                            </div>
                        </div>
                    </div>
                </nav>
            </div>
        </div>
    )
}