import { getProductById } from "../../Services/ProductService";
import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import { FaChevronRight } from "react-icons/fa";


export default function ProductDetail() {
    const { id } = useParams();
    const [productData, setProductData] = useState(null);
    const [selectedImage, setSelectedImage] = useState(null);
    const [active, setActive] = useState("collapseExample1");

    useEffect(() => {
        getProductById(id).then((res) => {
            const product = res.data[0];
            setProductData(product);

            if (product.productImages && product.productImages.length > 0) {
                setSelectedImage(product.productImages[0].imageUrl);
            }
        });
    }, [id])

    return (
        <>
            <div className="component">
                {productData ? (
                    <div className="detail-page" key={productData.id}>
                        <div className="link-list mt-3 mb-3">
                            <Link className="icon-and-links">Ürün Listesi <FaChevronRight className="icon-link" />
                                Ürün Detayı <FaChevronRight className="icon-link" /> {productData.productName} </Link>
                        </div>
                        <div className="row w-100 m-0 p-0">
                            <div className="col-md-5 detail-img">
                                {selectedImage && (
                                    <div className="detail-img-container">
                                        <div className="detail-img-container-cont">
                                            <img
                                                src={`/${selectedImage}`}
                                                className="card-img-top"
                                                alt={productData.productName}
                                            />
                                        </div>
                                    </div>
                                )}
                                <div className="images-list mt-2 d-flex gap-2">
                                    {productData.productImages.map((imageItem) => (
                                        <div className="card" key={imageItem.id}>
                                            <button
                                                type="button"
                                                className="border-0 bg-transparent p-0"
                                                onClick={() => setSelectedImage(imageItem.imageUrl)}>
                                                <img
                                                    src={`/${imageItem.imageUrl}`}
                                                    className="img-list-img"
                                                    alt={productData.productName}
                                                    style={{ cursor: "pointer" }} />
                                            </button>
                                        </div>
                                    ))}
                                </div>
                            </div>
                            <div className="col-md-7">
                                <div className="urun-baslik">{productData.productName}</div>

                                <div className="detail-price mt-3">{productData.price}</div>
                                <div className="item-quantity mt-3">Stok: {productData.quantity}</div>
                                <div className="item-collapseGroup mt-3">
                                    <div className="collapse-buttons">
                                        <p className="d-inline-flex gap-4 p-0 m-0">
                                            <a className={active === "collapseExample1" ? "nav-link active" : "nav-link"} data-bs-toggle="collapse" href="#collapseExample1" role="button" aria-expanded="true" aria-controls="collapseExample1"
                                                onClick={() => setActive("collapseExample1")}>
                                                Açıklama
                                            </a>
                                            <a className={active === "collapseExample2" ? "nav-link active" : "nav-link"} data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample2"
                                                onClick={() => setActive("collapseExample2")}>
                                                Soru & Cevaplar
                                            </a>
                                            <a className={active === "collapseExample3" ? "nav-link active" : "nav-link"} data-bs-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample3"
                                                onClick={() => setActive("collapseExample3")}>
                                                Değerlendirmeler
                                            </a>
                                        </p>
                                    </div>

                                    <div id="collapseGroup" className="item-detail-collapse">
                                        <div className={active === "collapseExample1" ? "collapse show active" : "collapse"} id="collapseExample1" data-bs-parent="#collapseGroup">
                                            <div className="card card-body">
                                                <div className="detail-explanation">{productData.explanation}</div>
                                                <div className="detail-explanation mt-3"><h5>Ürün Özellikleri</h5></div>
                                            </div>
                                        </div>
                                        <div className={active === "collapseExample2" ? "collapse show active" : "collapse"} id="collapseExample2" data-bs-parent="#collapseGroup">
                                            <div className="card card-body">
                                                <button type="button" className="btn saticiya-sor mb-3">Satıcıya sor</button>

                                                Soru & Cevaplar
                                            </div>
                                        </div>
                                        <div className={active === "collapseExample3" ? "collapse show active" : "collapse"} id="collapseExample3" data-bs-parent="#collapseGroup">
                                            <div className="card card-body">
                                                Değerlendirmeler
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>


                ) : (<div>Yükleniyor...</div>)}
            </div>
        </>


    )


}
