import { motion } from "framer-motion";

export default function CategoryUpdateModal({
    handleUpdate,
    handleUpdateInputChange,
    setshowModel,
    CategoryName
}) {
    return (
        <>
            <motion.div initial={{ opacity: 0, translateY: -30 }} animate={{ opacity: 1, translateY: 5 }} className="modal d-block" tabIndex="-1">
                <div className="modal-dialog modal-dialog-centered">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">Güncelleme</h5>
                            <button
                                type="button"
                                className="btn-close"
                                onClick={setshowModel}
                            ></button>
                        </div>

                        <div className="modal-body">
                            <div className="input-group">
                                <span className="input-group-text">Kategori ismi</span>
                                <input
                                    type="text"
                                    required
                                    className="form-control"
                                    name="CategoryName"
                                    value={CategoryName}
                                    onChange={handleUpdateInputChange}
                                />
                            </div>
                        </div>

                        <div className="modal-footer">
                            <button
                                type="button"
                                className="btn btn-secondary"
                                onClick={setshowModel}
                            >
                                İptal
                            </button>

                            <button
                                type="button"
                                className="btn btn-primary"
                                onClick={handleUpdate}
                            >
                                Güncelle
                            </button>
                        </div>
                    </div>
                </div>
            </motion.div>
            <div className="modal-backdrop fade show"></div>
        </>
    );
}