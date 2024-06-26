import React from "react";

interface InputProps {
  type: string;
  placeholder?: string;
  value?: string;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
  className?: string;
  disabled?: boolean;
  required?: boolean;
  id?: string;
  name?: string;
  min?: string;
}

const Input: React.FC<InputProps> = ({
  type,
  placeholder = "",
  value,
  onChange,
  className = "",
  disabled = false,
  required = false,
  id,
  name,
  min
}) => {
  return (
    <input
      type={type}
      placeholder={placeholder}
      value={value}
      onChange={onChange}
      className={`w-full p-2 rounded-lg bg-gray-50 border border-gray-300 text-gray-900 ${className}`}
      disabled={disabled}
      required={required}
      id={id}
      name={name}
      min={min}
    />
  );
};

export default Input;
