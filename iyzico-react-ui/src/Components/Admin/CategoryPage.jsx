import { useEffect, useState } from "react";
import { getCategories, addCategory, deleteCategory, updateCategory } from "../../Services/CategoryService";
import CostumSnackbar from "../Component/CostumSnackbar";
import DeleteModal from "../Component/DeleteModal";
import CategoryUpdateModal from "../Component/CategoryUpdateModal";
import { MdDelete } from "react-icons/md";
import { FaPencilAlt } from "react-icons/fa";
import { motion } from "framer-motion";

export default function CategoryPage() {
    const [categoryData, setCategoryData] = useState([]);
    const [snackBarDatas, setSnackbarDatas] = useState({
        isOpen: false,
        message: "",
        severity: ""
    });

    const [showModel, setShowModel] = useState(false);
    const [updateShowModel, setUpdateShowModel] = useState(false);

    useEffect(() => {
        getCategories().then((res) => setCategoryData(res.data));
    }, []);



    const [categoryNameData, setCategoryName] = useState({ CategoryName: "" });
    const [selectedCategory, setSelectedCategory] = useState();
    const [updatecategoryNameData, setUpdateCategoryName] = useState({ CategoryName: "" });

    const handleInputCategory = (e) => {
        const { name, value } = e.target;
        setCategoryName((prev) => ({
            ...prev,
            [name]: value,
        }));
    }

    const handleAddCategory = async () => {
        try {
            const res = await addCategory(categoryNameData);
            setSnackbarDatas({ isOpen: true, message: "İşlem başarılı.", severity: "success" })
            return res;
        } catch (error) {
            setSnackbarDatas({ isOpen: true, message: "İşlem gerçekleşirken bir hata oluştur. Lütfen daha sonra tekrar deneyin.", severity: "error" })
            console.log("Hata!!", error)
        };
    }


    const handleUpdateInputChange = (e) => {
        const { name, value } = e.target;
        setUpdateCategoryName({
            [name]: value
        });
    }

    const handleUpdateCategory = async (categoryId) => {
        try {
            await updateCategory(categoryId, updatecategoryNameData);
            setSnackbarDatas({ isOpen: true, message: "İşlem başarılı.", severity: "success" })
            setUpdateShowModel(false);
        } catch (error) {
            console.log("Hata detayı:", error.response?.data);
            setSnackbarDatas({ isOpen: true, message: "İşlem gerçekleşirken bir hata oluştur. Lütfen daha sonra tekrar deneyin.", severity: "error" })
            setUpdateShowModel(false);
        }
    };

    const handleSnackbarClose = () => {
        setSnackbarDatas({ isOpen: false })
    }
    const handleDeleteCategory = async (categoryId) => {
        try {
            const res = await deleteCategory(categoryId);
            setCategoryData((prevdata) => prevdata.filter((item) => item.id !== categoryId));
            setSnackbarDatas({ isOpen: true, message: "İşlem başarılı.", severity: "success" })
            setShowModel(false);
            return res;
        } catch (error) {
            console.log("Hala! ", error)
            setSnackbarDatas({ isOpen: true, message: "İşlem gerçekleşirken bir hata oluştur. Lütfen daha sonra tekrar deneyin.", severity: "error" })
            setShowModel(false);
            //alert("Kategori silinirken bir hata oluştu.", error.message);
        }
    }
    return (
        <>

            <motion.div initial={{ opacity: 0 }} animate={{ opacity: 1 }} exit={{ opacity: 0 }} className="component">

                <div className="page-baslik">Kategoriler</div>
                <div className="mb-3 mt-3">
                    <div className="add-item">
                        <div className="input-group">
                            <span className="input-group-text">Kategori ismi</span>
                            <input type="text" aria-label="Category Name" className="form-control" name="CategoryName" value={categoryNameData.CategoryName} onChange={handleInputCategory} />
                            <button type="button" className="btn btn-primary" onClick={handleAddCategory}>Ekle</button>
                        </div>
                    </div>
                </div>
                <div className="row w-100 p-0 m-0">
                    <div className="col-12 col-md-9 col-lg-12 mx-auto">
                        <div className="category-list-all" style={{ margin: "auto" }}>
                            <div className="text-end">
                                <label className="text-danger alert-label">*Kategori silinirken ilgili ürünlerde silinecektir</label>
                            </div>
                            <table className="table table-striped">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Kategori</th>
                                        <th>Ürün Sayısı</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {categoryData.map((item) => (
                                        <tr key={item.id}>
                                            <td>{item.id}</td>
                                            <td>{item.categoryName}</td>
                                            <td>Ürün Sayısı</td>
                                            <td>
                                                <div className="ms-auto">
                                                    <button
                                                        type="button"
                                                        className="btn btn-warning delete-btn me-1 ms-1"
                                                        onClick={() => {
                                                            setUpdateShowModel(true);
                                                            setSelectedCategory(item.id);
                                                            setUpdateCategoryName({
                                                                CategoryName: item.categoryName
                                                            });
                                                        }}><FaPencilAlt/></button>

                                                    {updateShowModel && (
                                                        <CategoryUpdateModal
                                                            handleUpdate={() => handleUpdateCategory(selectedCategory)}
                                                            handleUpdateInputChange={handleUpdateInputChange}
                                                            setshowModel={() => setUpdateShowModel(false)}
                                                            CategoryName={updatecategoryNameData.CategoryName}
                                                        />
                                                    )}

                                                   
                                                    <button type="button" className="btn btn-danger delete-btn me-1 ms-1" onClick={() => setShowModel(true)}><MdDelete /></button>
                                                    {showModel && (
                                                        <DeleteModal
                                                            handleDeleteId={() => handleDeleteCategory(item.id)}
                                                            setshowModel={() => setShowModel(false)}>
                                                        </DeleteModal>
                                                    )}
                                                </div>
                                            </td>
                                        </tr>
                                    ))}
                                    
                                </tbody>
                            </table>
                           
                        </div>
                    </div>
                </div>

                <CostumSnackbar open={snackBarDatas.isOpen}
                    snackbarClose={handleSnackbarClose}
                    severity={snackBarDatas.severity}
                    message={snackBarDatas.message}>
                </CostumSnackbar>
            </motion.div>
        </>

    )
}