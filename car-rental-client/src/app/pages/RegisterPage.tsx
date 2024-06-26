import React, { useState } from "react";
import Input from "../ui/input/Input";
import Button from "../ui/button/Button";
import { register } from "../../api/services/authService";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function RegisterPage() {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    confirmPassword: ""
  });
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);
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
      await register(formData);
      navigate("/sign-in");
    } catch (error) {
      if (axios.isAxiosError(error)) {
        setError(error.response?.data.errorMessage);
      } else {
        setError("Registration failed. Please try again.");
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex justify-center">
      <form className="w-80 sm:w-[400px] justify-center" onSubmit={handleSubmit}>
        <div className="flex justify-center text-3xl my-4">
          <span>Sign up</span>
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
        <div className="py-4">
          <div className="flex justify-center text-xl">
            <label htmlFor="confirmPassword">Confirm password</label>
          </div>
          <Input
            type="password"
            name="confirmPassword"
            placeholder="Confirm password"
            required={true}
            value={formData.confirmPassword}
            onChange={handleInputChange}
          />
        </div>
        <div className="flex justify-center pt-8 pb-4">
          <Button isLoading={isLoading}>Sign up</Button>
        </div>
        {error && <div className="text-red-500 text-center">{error}</div>}
      </form>
    </div>
  );
}
