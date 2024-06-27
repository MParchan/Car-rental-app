import React, { useEffect, useState } from "react";
import ModelDatail from "../components/models/modelDetail/ModelDatail";
import { useNavigate, useParams } from "react-router-dom";
import { getModel } from "../../api/services/modelService";
import { Model } from "../../types/models/model.types";
import NotFoundPage from "./NotFoundPage";
import Loader from "../ui/loader/Loader";

export default function CarModelPage() {
  const { brandModel, id } = useParams();
  const navigate = useNavigate();
  const [model, setModel] = useState<Model | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchModel = async () => {
      try {
        const fetchedModel = await getModel(Number(id));
        setModel(fetchedModel);
        setLoading(false);
      } catch (error) {
        setLoading(false);
      }
    };

    fetchModel();
  }, [id]);

  useEffect(() => {
    if (model && brandModel) {
      const urlModel = (model.brand?.name + "-" + model.name).toLowerCase().replace(/ /g, "-");
      if (urlModel !== brandModel) {
        navigate(`/m/${urlModel}/${id}`);
      }
    }
  }, [model, brandModel, id, navigate]);

  if (loading) {
    return <Loader />;
  }

  if (!model) {
    return <NotFoundPage />;
  }

  return (
    <div>
      <ModelDatail model={model} />
    </div>
  );
}
