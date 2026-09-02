import { useState, useEffect } from "react";
import { getProducts } from "../../Services/ProductService";

export default function ProductList() {
    const [productData, setProductData] = useState([]);

    useEffect(() => {
        getProducts().then((res) => setProductData(res.data));
    }, [])
    return (
        <>
            <div className="component">
                <div className="row m-0 p-0">
                    <div className="col-md-3 col-lg-4 col-12">
                        {productData.map((item) => (
                            <div className="card" style={{ width: '18rem' }}>
                                <img src={`/${item.productImages?.[0]?.imageUrl}`} alt={item.productName} />
                                <div className="card-body">
                                    <h5 className="card-title">{item.productName}</h5>
                                    <p className="card-text">{item.explanation}</p>
                                    <a href="#" className="btn btn-primary">Go somewhere</a>
                                </div>
                            </div>
                        ))}

                    </div>
                </div>
            </div>

        </>
    )
}