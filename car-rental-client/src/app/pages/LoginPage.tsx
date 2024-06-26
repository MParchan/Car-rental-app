import React, { useContext, useState } from "react";
import Button from "../ui/button/Button";
import Input from "../ui/input/Input";
import { useNavigate } from "react-router-dom";
import { login } from "../../api/services/authService";
import axios from "axios";
import { AuthContext } from "../contexts/authContext/AuthContext";

export default function LoginPage() {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    confirmPassword: ""
  });
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const { setLoggedIn } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError(null);

    try {
      await login(formData);
      setLoggedIn(true);
      navigate("/");
    } catch (error) {
      if (axios.isAxiosError(error)) {
        setError(error.response?.data.errorMessage);
      } else {
        setError("Login failed. Please try again.");
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex justify-center">
      <form className="w-60 sm:w-80 justify-center" onSubmit={handleSubmit}>
        <div className="flex justify-center text-3xl my-4">
          <span>Sign in</span>
        </div>
        <div className="py-4">
          <div className="flex justify-center text-xl">
            <label htmlFor="email">Email</label>
          </div>
          <Input
            type="email"
            name="email"
            placeholder="Email"
            required={true}
            value={formData.email}
            onChange={handleInputChange}
          />
        </div>
        <div className="py-4">
          <div className="flex justify-center text-xl">
            <label htmlFor="password">Password</label>
          </div>
          <Input
            type="password"
            name="password"
            placeholder="Password"
            required={true}
            value={formData.password}
            onChange={handleInputChange}
          />
        </div>
        <div className="flex justify-center pt-8 pb-4">
          <Button isLoading={isLoading}>Sign in</Button>
        </div>
        {error && <div className="text-red-500 text-center">{error}</div>}
      </form>
    </div>
  );
}
